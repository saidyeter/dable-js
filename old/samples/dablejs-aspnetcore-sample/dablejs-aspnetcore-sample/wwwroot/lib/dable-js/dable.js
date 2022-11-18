class DableJS {
    #dableId = null
    #hostId = null
    #sourceUrl = null
    #columns = null
    #initCallback = null
    #renderCallback = null
    #totalCount = 0
    #dataList = []
    #pageIndex = 0
    #recordPerPage = 10
    #orderColumnName = ""
    #orderAsc = true

    /**
     * @param {Array<Column>} columns Column definitions
     * @param {Function} initCallback
     */

    /**
     * @param {string} hostId Container(eg:div) id to place generated data
     * @param {string} sourceUrl Url for fecthing data from
     * @param {any} options
     */
    constructor(hostId, sourceUrl, options) {
        this.#dableId = this.#generateId()
        this.#hostId = hostId
        this.#sourceUrl = sourceUrl
        if (options != undefined) {
            if (options.initCallback != undefined) {
                this.#initCallback = options.initCallback
            }
            if (options.renderCallback != undefined) {
                this.#renderCallback = options.renderCallback
            }
            if (options.columns != undefined) {
                this.#columns = options.columns
            }
            if (options.recordPerPage != undefined) {
                this.#recordPerPage = options.recordPerPage
            }
        }
    }

    async init() {
        await this.#fetchData()
        if (this.#totalCount == 0) {
            if (this.#columns == null) { return }
        }
        if (this.#columns == null) {
            var columnNamesFromData = Object.getOwnPropertyNames(this.#dataList[0])
            this.#columns = columnNamesFromData.map(val => {
                return {
                    type: "data",
                    title: this.#convertToTitle(val),
                    property: val
                }
            })
        }
        var div = this.#createContainer()
        document.getElementById(this.#hostId).appendChild(div)
        if (this.#initCallback !== null) { this.#initCallback() }
    }

    #createContainer() {
        var containerDiv = this.#new('div', ['dable', 'dable-container'])
        containerDiv.id = this.#dableId
        var headerDiv = this.#createContainerHeader()
        containerDiv.appendChild(headerDiv)
        var bodyDiv = this.#createContainerBody()
        containerDiv.appendChild(bodyDiv)
        var footerDiv = this.#createContainerFooter()
        containerDiv.appendChild(footerDiv)
        return containerDiv
    }

    #countMessage() {
        var start = (this.#pageIndex * this.#recordPerPage) + 1
        var end = (this.#pageIndex + 1) * this.#recordPerPage
        if (end > this.#totalCount) { end = this.#totalCount }
        return `${this.#totalCount} kayıt içinden ${start} - ${end} arası gösteriliyor.`
    }
    #createContainerHeader() {
        var headerDiv = this.#new('div', ['dable', 'dable-header-div'])
        //TODO: searchbar eklenecek 
        //var searchBar = this.#createSearchBar()
        //headerDiv.appendChild(searchBar)
        var countMessageSpan = this.#new('span', ['dable', 'dable-header-count-message'])
        countMessageSpan.textContent = this.#countMessage()
        headerDiv.appendChild(countMessageSpan)
        return headerDiv
    }
    #createContainerBody() {
        var bodyDiv = this.#new('div', ['dable', 'dable-body-div'])
        var table = this.#createTable()
        bodyDiv.appendChild(table)
        table.appendChild(this.#renderTbody())
        return bodyDiv
    }
    #createContainerFooter() {
        var footerDiv = this.#new('div', ['dable', 'dable-footer-div'])
        for (var i = 0; i < Math.ceil(this.#totalCount / this.#recordPerPage); i++) {
            var span = this.#new('span', ['dable', 'dable-page-number'])
            const num = i;
            span.textContent = (num + 1).toString()
            span.onclick = () => {
                this.#pageIndex = num
                this.#reRender()
            }
            if (num == 0) {
                span.classList.add('dable-page-selected')
            }
            span.style.cursor = "pointer"
            footerDiv.appendChild(span)
        }
        return footerDiv
    }

    #createSearchBar() {
        var input = this.#new("input")
        input.type = "text"
        input.placeholder = "Ara"
        input.style.width = "100%"
        return input
    }
    #createTable() {
        var table = this.#new('table', ['dable', 'dable-table'])
        var header = table.createTHead()
        var tr = this.#new('tr', ['dable', 'dable-header', 'dable-row'])
        header.appendChild(tr)
        this.#columns.forEach(column => {
            var td = this.#new('td', ['dable', 'dable-header', 'dable-col'])
            td.innerText = column.title
            if (column.type == "data") {
                td.style.cursor = "pointer"
                td.onclick = () => {
                    if (this.#orderColumnName != column.property) { this.#orderAsc = true }
                    else { this.#orderAsc = !this.#orderAsc }
                    this.#orderColumnName = column.property
                    var headerTdList = document.querySelectorAll(`div#${this.#dableId} > div > table.dable-table > thead > tr > td.dable-col`)
                    headerTdList.forEach(element => {
                        if (element.textContent.endsWith(' ▲') || element.textContent.endsWith(' ▼')) {
                            element.textContent = element.textContent.replace(' ▲', '').replace(' ▼', '')
                            element.style.fontStyle = "normal";
                        }
                    })
                    if (this.#orderAsc) { td.textContent = td.textContent + ' ▼' }
                    else { td.textContent = td.textContent + ' ▲' }
                    td.style.fontStyle = "italic";
                    this.#reRender()
                }
            }
            tr.appendChild(td)
        })
        return table;
    }
    #renderTbody() {
        var tbody = document.createElement('tbody')
        this.#dataList.forEach((listValue, _) => {
            var tr = document.createElement('tr')
            tr.classList.add('dable')
            tr.classList.add('dable-body')
            tr.classList.add('dable-row')
            this.#columns.forEach((colValue, _) => {
                var td = document.createElement('td')
                td.classList.add('dable')
                td.classList.add('dable-body')
                td.classList.add('dable-col')
                if (colValue.type == "data") {
                    td.innerText = listValue[colValue.property]
                } else if (colValue.type == "action") {
                    td.innerHTML = new Function("return `" + colValue.content + "`;").call(listValue)
                    //td.innerHTML = this.#fillTemplate(colValue.content, listValue)
                }
                tr.appendChild(td)
            })
            tbody.appendChild(tr)
        })
        if (this.#renderCallback !== null) { this.#renderCallback() }
        return tbody
    }
    //#fillTemplate = function (templateString, templateVars) { return new Function("return `" + templateString + "`;").call(templateVars); }

    async #reRender() {
        await this.#fetchData()
        if (this.#totalCount == 0) {
            return
        }
        var tbody = this.#renderTbody()
        var table = document.querySelector(`div#${this.#dableId} > div > table.dable-table`)
        table.removeChild(table.tBodies[0])
        table.appendChild(tbody)
        var span = document.querySelector(`div#${this.#dableId} > div > span.dable-header-count-message`)
        span.textContent = this.#countMessage()
        var pageNumbers = document.querySelectorAll(`div#${this.#dableId} > div > span.dable-page-number`)
        pageNumbers.forEach(item => item.classList.remove('dable-page-selected'))
        var activePage = pageNumbers[this.#pageIndex]
        activePage.classList.add('dable-page-selected')
    }

    #convertToTitle(columnName) {
        var arr = columnName.split('').map((item, index) => {
            if (index == 0 && !/[a-z]/.test(item)) return ''
            if (index == 0 && /[a-z]/.test(item)) return item.toUpperCase()
            else if (index != 0 && /[A-Z]/.test(item)) return ' ' + item
            else if (item == "_") return ' '
            else return item
        })
        return arr.join('').replace('  ', ' ')
    }

    #generateId() {
        var res = new Date().getTime().toString()
        return `d${res}d`
    }

    #new(tag, classList) {
        var element = document.createElement(tag)
        if (classList != undefined) {
            classList.forEach(c => element.classList.add(c))
        }
        return element
    }

    async #fetchData() {

        var requestBody = {
            pageIndex: this.#pageIndex,
            recordPerPage: this.#recordPerPage,
            orderColumnName: this.#orderColumnName,
            orderAsc: this.#orderAsc
        }

        try {
            var response = await fetch(this.#sourceUrl, {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                mode: 'cors', // no-cors, *cors, same-origin
                cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
                credentials: 'same-origin', // include, *same-origin, omit
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                },
                redirect: 'follow', // manual, *follow, error
                referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
                body: JSON.stringify(requestBody) // body data type must match "Content-Type" header
            });
            var data = await response.json(); // parses JSON response into native JavaScript objects


            this.#dataList = data.list
            this.#totalCount = data.totalCount

        } catch (err) {
            console.error("dable-js getData error: ", err)
            this.#dataList = []
            this.#totalCount = 0
        }
    }


    //openAccount(bank) {
    //    this.#account = bank.openAccount(this)
    //}

    //withdraw(amount) {
    //    this.#account.withdraw(amount)
    //}

    //deposit(amount) {
    //    this.#account.deposit(amount)
    //}

    //get account() {
    //    return this.#account
    //}

    //get name() {
    //    return this.#name
    //}
}
