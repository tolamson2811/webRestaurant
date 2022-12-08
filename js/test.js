var authRegister = "http://localhost:54634/api/Auth/register";

function register(data) {
  var options = {
    method: "POST",
    mode: 'cors',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  };
  fetch(authRegister, options)
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
    });
}

var form = {
    username: 'sonsonson',
    password: '123456',
    role: '2',
    userId: '0'
}

register(form)