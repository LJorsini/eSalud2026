let linkApi = 'http://localhost:5195/Api';

document.getElementById("formularioRegistro").addEventListener("submit", async (e) => {
    e.preventDefault();

    console.log("registro cargo");

    document.getElementById("errorNombre").textContent ="";
    document.getElementById("errorEmail").textContent ="";
    document.getElementById("errorDni").textContent = "";

    let nombreCompleto = document.getElementById("nombreCompleto").value.trim();
    let email = document.getElementById("email").value.trim();
    let dni = document.getElementById("dni").value.trim();
    

    const datosUsuarios = {
        NombreCompleto : nombreCompleto,
        Email : email,
        DNI : dni
    }

    let campoCompleto = true;

    if(!nombreCompleto)
    {
        document.getElementById("errorNombre").textContent = "Falta ingresar el nombre";
        compoCompleto = false;
    }

    if(!email)
    {
        document.getElementById("errorEmail").textContent = "Falta ingresar el Email";
        campoCompleto = false;
    }

    if(!dni)
    {
        document.getElementById("errorDni").textContent = "Falta el nombre ingresar el DNI";
        campoCompleto = false;
    }

    if(!campoCompleto)
    {
        return;
    }

    try 
    {
        const respuesta = await fetch(`${linkApi}/auth/registrar`, {
        method : "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(datosUsuarios)

    });

    const resultado = await respuesta.text();
    if (respuesta.ok)
    {
        /* alert(resultado) */
        await Swal.fire({
        position: "center",
        icon: "success",
        title: resultado,
        showConfirmButton: false,
        timer: 1500
        });
        
        window.location.href = "signin.html";
    }
    else {
        console.error(resultado);
       
        /* alert("Registro Fallido" + resultado); */
        const error = resultado
        Swal.fire({
        icon: "error",
        title: "Oops...",
        text: error,
        
    });
    }
    } catch (error) {
        console.error(error);
        alert("No se pudo conectar con el servidor. Intentá de nuevo más tarde.");
    }

    

    
});