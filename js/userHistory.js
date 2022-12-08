var container = document.getElementById('history-buy')
var purchaseApi = 'http://localhost:54634/api/Purchase'

function start() {
    getStore(renderStore)
}

start()

function getStore(callback) {
    fetch(purchaseApi)
        .then((response) => response.json())
        .then(callback)
}

function renderStore(stores) {
    var htmls = stores.data.map((store) => {
        if (store.userId == localStorage.getItem('userId')) {
            return `
            <div class="col-2 text-center">
            <p class="">${store.purchaseDate}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.description}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.total}</p>
          </div>
          <div class="col-2 text-center"
            <p>${store.fullName}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.address}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.phoneNumber}</p>
          </div>
            ` 
        } else if(localStorage.getItem('userId') == 32) {
            return `
            <div class="col-2 text-center">
            <p class="">${store.purchaseDate}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.description}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.total}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.fullName}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.address}</p>
          </div>
          <div class="col-2 text-center">
            <p>${store.phoneNumber}</p>
          </div>
            ` 
        } else {
            return ""
        }
    })
    container.innerHTML = htmls.join('')
}