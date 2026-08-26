let linkApi = 'http://localhost:5195/Api';

document.getElementById("formularioRegistro").addEventListener("submit", async (e) => {
    e.preventDefault();

    console.log("registro cargo");

    const datosUsuarios = {
        NombreCompleto : document.getElementById("nombreCompleto").value,
        Email : document.getElementById("email").value,
        DNI : document.getElementById("dni").value
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
        alert(resultado)
        /* window.location.href = "../../views/usuarios/signin.html"; */
    }
    else {
        console.error(resultado);
        alert("Registro Fallido" + resultado);
    }
    } catch (error) {
        console.error(error);
        alert("No se pudo conectar con el servidor. Intentá de nuevo más tarde.");
    }

    

    
});