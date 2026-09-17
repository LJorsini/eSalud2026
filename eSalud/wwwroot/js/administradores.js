async function GetAdministradores()
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });


    try {
        const resAdministradores = await fetch(`${linkApi}/administrador`, {
        method: "GET",
        headers: authHeaders(),
    });

    const respuesta = await resAdministradores.json();

    } catch (error) {
        
    }
    
}