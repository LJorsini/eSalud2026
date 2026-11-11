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

    /* const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    }); */

    document.getElementById("errorNombreCompleto").textContent = "";
    document.getElementById("errordni").textContent = "";
    document.getElementById("errorFechaNacimiento").textContent = "";
    document.getElementById("errorGenero").textContent = "";
    document.getElementById("errorDireccion").textContent = ""; 
    document.getElementById("errorEmail").textContent = "";
    document.getElementById("errorSeleccionProvincia").textContent = "";
    document.getElementById("errorSeleccionLocalidad").textContent = "";
    
    



    const administradorId = document.getElementById("administradorId").value;
    const nombreAdministrador = document.getElementById("nombreAdministrador").value.trim();
    const dniAdministrador = document.getElementById("dniAdministrador").value.trim();
    const fechaNacimiento = document.getElementById("fechaNacimientoAdministrador").value;
    const sexo = document.getElementById("generoAdministrador").value;
    const direccion = document.getElementById("direccionAdministrador").value.trim();
    const telefono = document.getElementById("telefonoAdministrador").value.trim();
    const email = document.getElementById("email").value.trim();
    const provincia = document.getElementById("selectProvincias").value;
    const localidadId = document.getElementById("selectLocalidad").value;

    let campoCompleto = true;

    if(!nombreAdministrador)
    {
        document.getElementById("errorNombreCompleto").innerHTML = "Falta el nombre";
        campoCompleto = false;
    }

    if(!dniAdministrador)
    {
        document.getElementById("errordni").innerHTML = "Falta el dni";
        campoCompleto = false;
    }

    if(!fechaNacimiento)
    {
        document.getElementById("errorFechaNacimiento").innerHTML = "Ingrese la fecha de nacimiento";
        campoCompleto = false;
    }

    if(!sexo)
    {
        document.getElementById("errorGenero").innerHTML = "Ingrese el genero";
        campoCompleto = false;
    }

    if(!direccion)
    {
        document.getElementById("errorDireccion").innerHTML = "Ingrese la direccion";
        campoCompleto = false;
    }

    if(!telefono)
    {
        document.getElementById("errortelefono").innerHTML = "Ingrese el telefono";
        campoCompleto = false;
    }

    if(!email)
    {
        document.getElementById("errorEmail").innerHTML = "Ingrese el email";
        campoCompleto = false;
    }


    if(!provincia)
    {
        document.getElementById("errorSeleccionProvincia").innerHTML = "Seleccione una provincia";
        campoCompleto = false;
    }

    if(!localidadId)
    {
        document.getElementById("errorSeleccionLocalidad").innerHTML = "Seleccione una localidad";
        campoCompleto = false;
    }

    if(!campoCompleto)
    {
        return
    }

    const administrador = {

        /* AdministradorId: administradorId, */
        NombreCompleto: nombreAdministrador,
        Dni: dniAdministrador,
        FechaNacimiento: fechaNacimiento,
        Sexo: sexo,
        Direccion: direccion,
        PhoneNumber: telefono,
        Email = email,
        LocalidadId: localidadId,

    }

    console.log(administrador)

    const result = await Swal.fire({
                   title: "¿Desea crear el administrador?",
                   showCancelButton: true,
                   confirmButtonText: "Save",
    });

    if(result.isConfirmed)
    {
        try {
        const res = await fetch(`${linkApi}/administrador`, {
            method: "POST",
            headres: authHeaders(),
            body: JSON.stringify(administrador),
        });

        if(!res.ok)
        {
            throw new error(`Error del servidor: ${response.status}`);
        }

        await Swal.fire("!Administrador Creado!", "", "success")

        ObtenerAdministradores();
    } catch (error) {
         console.error("Error al guardar localidad:", error);
         Swal.fire("Error", "No se pudo guardar la localidad", "error");
    }
    }
    







}

document.getElementById("selectProvincias").addEventListener("change", (e) => {
    document.getElementById("errorSeleccionLocalidad").textContent = "";
     ObtenerLocalidades(e.target.value);
})

ObtenerProvincias();
ObtenerAdministradores()