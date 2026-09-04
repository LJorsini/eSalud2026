let linkApi = 'http://localhost:5195/Api';

function UsuarioLogueado()
{
    try {

        const respuesta = fetch(`${linkApi}/usuarioLogueado`, {
            method: "GET",
            headers: {"Content-Type": "application/json"},
        });

        const resultado = respuesta.json();

        console.log(resultado);
        
    } catch (error) {
        
    }
}