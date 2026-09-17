let linkApi = 'http://localhost:5195/Api';

function getToken() {
    return localStorage.getItem("token");
}

function getRefreshToken() {
    return localStorage.getItem("refreshToken");
}

function getEmail() {
    return localStorage.getItem("email");
}

function getDatosUsuarios()
{
  const token = getToken();

  if(!token)
  {
    return null;
  }

  const playload = JSON.parse(atob(token.split('.')[1]));

  return {
    id: playload.id,
    userName: playload.userName,
    email: playload.email,
    nombreCompleto: playload.NombreCompleto,
  };
 
  
}

/* function getNombreCompleto()
{
  return localStorage.getItem("nombreCompleto")
} */


function saveTokens(token, refreshToken) {
    localStorage.setItem("token", token);
    localStorage.setItem("refreshToken", refreshToken);
}

function refreshToken() {
  return fetch(URL_BASE_API + "refresh-token", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      email: getEmail(),
      refreshToken: getRefreshToken()
    })
  })
  .then(function(response) {
    if (!response.ok) {
      throw new Error("Error al renovar el token");
    }
    return response.json();
  })
  .then(function(data) {
    saveTokens(data.token, data.refreshToken);
    return data.token;
  });
}