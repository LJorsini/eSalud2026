
/* dropdawn provincias */
async function ObtenerProvincias()
{
    console.log("Scrip carga")
    
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

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

document.getElementById("modalLocalidad").addEventListener("hidden.bs.modal", function () {
    LimpiarModalLocalidad();
});

async function ObtenerLocalidades()
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    try {

        const res = await fetch(`${linkApi}/localidades`, {
            method: "GET",
            headerrs: authHeaders(),
        });

        if(!res.ok)
        {
            Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Error al obtener las provincias",
            
        });
        }
        const resLocalidades = await res.json();

        console.log(resLocalidades);

        LimpiarModalLocalidad();
        $("#modalLocalidad").modal("hide");

        const tablaVacia = document.getElementById("mensajeTablaVacia")
        tablaVacia.innerHTML = ""

        if(resLocalidades.length === 0)
        {
            tablaVacia.innerHTML = "No hay localidades cargadas";
        }

        const tbodyLocalidades = document.getElementById("tbodyLocalidades");
        tbodyLocalidades.innerHTML = "";

        resLocalidades.forEach(localidad => {

            const row = document.createElement("tr");
            row.classList.add("align-middle") 

            row.innerHTML = `
                <td>${localidad.nombreLocalidad}</td>
                <td>${localidad.nombreProvincia.toUpperCase()}</td>
                <td>
                <button class="btn btn-primary" onclick="AbrirModalEditar(${localidad.localidadId})">Editar</button>
                </td>
            
            `;
            tbodyLocalidades.appendChild(row);
            
        });


    } catch (error) {
        console.error(error);
    }

    
}

function CargarEditarLocalidad()
{
    var id = document.getElementById("localidadId").value;

    if (id == 0)
    {
        CrearLocalidad();
    } else {
        EditarLocalidad(id);
    }


}

document.querySelectorAll(".mayuscula").forEach(input => {
    input.addEventListener("input", function () {
        this.value = this.value.toUpperCase();
    });
});

async function CrearLocalidad()
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    /* LimpiarModalLocalidad(); */

    document.getElementById("errorNombreLocalidad").textContent = "";
    document.getElementById("errorSeleccionProvincia").textContent = "";
    document.getElementById("errorCP").textContent = "";

    let nombreLocalidad = document.getElementById("nombreLocalidad").value.trim();
    nombreLocalidad = nombreLocalidad.toUpperCase();

    let cpLocalidad = document.getElementById("cpLocalidad").value.trim();
    let provinciaId = document.getElementById("selectProvincias").value;
    

    let campoCompleto = true;

    if(!nombreLocalidad)
    {
        document.getElementById("errorNombreLocalidad").innerHTML = "Falta el nombre de la localidad";
        campoCompleto = false;
    }

    if(!cpLocalidad)
    {
        document.getElementById("errorCP").innerHTML = "Falta ingresar el codigo postal";
        campoCompleto = false;
    }

    if(provinciaId == "")
    {
        document.getElementById("errorSeleccionProvincia").innerHTML = "Ingrese la provincia";
        campoCompleto = false;
    }

    if(!campoCompleto)
    {
        return
    }

    const localidad = {
        NombreLocalidad: nombreLocalidad,
        ProvinciaId:  provinciaId,
        CP: cpLocalidad,
    };


    console.log(localidad)
    console.log("funciona guardar")

    const result = await Swal.fire({
                   title: "¿Desea guardar la localidad?",
                   showCancelButton: true,
                   confirmButtonText: "Save",
    });

    if(result.isConfirmed)
    {
        try {
            const respuesta = await fetch(`${linkApi}/localidades`, {
                method: "POST",
                headers: authHeaders(),
                body: JSON.stringify(localidad)
            });

            if (!respuesta.ok)
            {
                throw new error(`Error del servidor: ${response.status}`);
            }

            await Swal.fire("!Provincia guardada!", "", "success")

        } catch (error) {
            console.error("Error al guardar localidad:", error);
            Swal.fire("Error", "No se pudo guardar la localidad", "error");
        }
        console.log("confirmado")

        ObtenerLocalidades();
    }
}

async function AbrirModalEditar(idLocalidad)
{
    
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    $("#modalLocalidad").modal("show");
    
    

    try {

        const res = await fetch(`${linkApi}/localidades/${idLocalidad}`, {
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

    const resLocalidad = await res.json();

    document.getElementById("localidadId").value = resLocalidad.localidadId;
    document.getElementById("nombreLocalidad").value = resLocalidad.nombreLocalidad;
    document.getElementById("cpLocalidad").value = resLocalidad.cp;
    document.getElementById("selectProvincias").value = resLocalidad.provinciaId;



    
    console.log(resLocalidad);

    } catch (error) {
        Swal.fire("Error", "No se pudo mostrar la localidad", "error");
    }

    
    
}

function LimpiarModalLocalidad()
{
    document.getElementById("localidadId").value = 0
    document.getElementById("nombreLocalidad").value = "";
    document.getElementById("cpLocalidad").value = "";
    document.getElementById("selectProvincias").value = "";
}





ObtenerProvincias();
ObtenerLocalidades();