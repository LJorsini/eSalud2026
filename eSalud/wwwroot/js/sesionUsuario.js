function verificarUsuario(){
    const token = getToken();
    const email = getEmail(); // suponiendo que guardaste el email al hacer login
   //console.log(email);
   document.getElementById("email-usuario").textContent = email; 
   //$("#email-usuario").text(email);

    if (!token) {
        localStorage.removeItem("token");
        localStorage.removeItem("email");
        window.location.href = "signin.html";
        return;
    }
}  