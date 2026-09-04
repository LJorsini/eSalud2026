let linkApi = 'http://localhost:5195/Api';


document.getElementById("loginForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    console.log("registro cargo")

    document.getElementById("errorEmail").textContent = "";
    document.getElementById("errorPassword").textContent = "";

    let email = document.getElementById("email").value.trim();
    let password = document.getElementById("password").value.trim();

    const datosLogin = {
        Email : email,
        Password : password
    }

    let campoCompleto = true;

    if(!email)
    {
        document.getElementById("errorEmail").textContent = "Ingrese el Email";
        campoCompleto = false;
    }

    if(!password)
    {
        document.getElementById("errorPassword").textContent = "Ingrese la contraseña";
        campoCompleto = false;
    }

    if(!campoCompleto)
    {
        return
    }

    try 
    {
        const respuesta = await fetch(`${linkApi}/auth/login`, {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(datosLogin)
        });

        const resultado = await respuesta.json();

        if(!resultado.success)
        {
            var error = resultado.message;
            Swal.fire({
            icon: "error",
            title: "Oops...",
            text: error,
            /* footer: "<a href=\"#\">Why do I have this issue?</a>" */
            });
            /* errorContainer.textContent = resultado.message; */
            return;
        }

        localStorage.setItem("token", resultado.token);
        localStorage.setItem("refreshToken", resultado.refreshToken);
        localStorage.setItem("email", datosLogin.Email);

        window.location.href = "index.html";

    } catch (error) {
         Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "No fue posible conectarcon el servidor",
            
            });
    }
    
});

function getToken() {
    return localStorage.getItem("token");
}

