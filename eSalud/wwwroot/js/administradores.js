async function ObtenerProvincias()
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });
    console.log("Scrip carga")
    try {
        
        const res = await fetch(`${linkApi}/provincias`, {
            method: "GET",
            headers:authHeaders(), 
        })

        const provincias = await res.json();
        console.log(provincias);

        const selectProvincias = document.getElementById("selectProvincias")
        selectProvincias.innerHTML = ""

        let opciones = `<option value="" >-- Seleccione la provincia --</option>`

        provincias.forEach(provincia => {
            /* const option = document.createElement("option")

            option.value = provincia.provinciaId;
            option.textContent = provincia.nombreprovincia

            selectProvincias.appendChild(option); */
            opciones += `<option value="${provincia.provinciaId}">${provincia.nombreProvincia}</option>`
            
        });

        selectProvincias.innerHTML = opciones;

    } catch (error) {
        
    }
}

async function ObtenerLocalidades(provinciaId)
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    try {
        
        const res = await fetch(`${linkApi}/localidades/localidades/${provinciaId}`, {
            method: "GET",
            headers: authHeaders(),
        });

        if(!res.ok)
        {
            Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Error al obtener las localidades",
            });
        }

        const localidades = await res.json();
        console.log(localidades)

        const selectLocalidades = document.getElementById("selectLocalidad");
        selectLocalidades.innerHTML = ""

        let opciones = `<option value="" >-- Seleccione la localidad --</option>`

        if(localidades.length === 0)
        {
            opciones = `<option value="">-- No existe ninguna localidad para la provincia --</option>`
            selectLocalidades.style.color = "red"
        } else {
            selectLocalidades.style.color = ""; // vuelve al color normal
        }

        localidades.forEach(localidad => {
            opciones += `<option value="${localidad.localidadId}">${localidad.nombreLocalidad}</option>`
        });

        selectLocalidades.innerHTML = opciones;
        




    } catch (error) {
        console.error(error);
    }
}


async function ObtenerAdministradores()
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

    if(!resAdministradores.ok)
    {
        throw new error(`Error del servidor: ${response.status}`);
    }

    const administradores = await resAdministradores.json();
    console.log(administradores);

    if(administradores.length === 0)
    {
        document.getElementById("tbodyAdministradores").innerHTML = "No existen administradores"
    }

    const tbodyAdministradores = document.getElementById("tbodyAdministradores");
    tbodyAdministradores.innerHTML = "";

    administradores.forEach(administrador => {

        var fechaNacimiento = administrador.fechaNacimiento;
        console.log(fechaNacimiento)

        if(fechaNacimiento == null)
        {
            fechaNacimiento = "---"
        } else
        {
            fechaNacimiento == campoDni
        };

        
        const row = document.createElement("tr")
        row.classList.add("align-middle")

        row.innerHTML = `
               <td>${administrador.nombreCompleto}</td>
               <td>${fechaNacimiento}</td>
               <td>${administrador.dni}</td>
               <td>
                    <button class="btn btn-primary" onclick="AbrirModalEditar(${administrador.administradorId})">Editar</button>
               </td>
               <td>
                    <button class="btn btn-primary" onclick="AbrirModalEditar(${administrador.administradorId})">Deshabilitar</button>
            </td>

        `;

        tbodyAdministradores.appendChild(row);
        
    });

    } catch (error) {
        console.error(error);
    }
    
}

function CrearEditarAdministrador (id)
{
    var id = document.getElementById("administradorId").value;


    if(id == 0)
    {
        CrearAdministrador();
    } else {
        EditarAdministrador(id);
    }
}

async function CrearAdministrador()
{
    console.log("funciona crear adminstrador")

    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    const administradorId = document.getElementById("administradorId").value;
    const nombreAdministrador = document.getElementById("nombreAdministrador").value.trim();
    const dniAdministrador = document.getElementById("dniAdministrador").value.trim();
    const fechaNacimiento = document.getElementById("fechaNacimientoAdministrador").value();
    const sexo = document.getElementById("generoAdministrador").value;
    const direccion = document.getElementById("direccionAdministrador").value.trim();
    const provincia = document.getElementById("selectProvincias").value;
    const localidadId = document.getElementById("selectLocalidad").value;


}

document.getElementById("selectProvincias").addEventListener("change", (e) => {
    document.getElementById("errorSeleccionLocalidad").textContent = "";
     ObtenerLocalidades(e.target.value);
})

ObtenerProvincias();
ObtenerAdministradores()