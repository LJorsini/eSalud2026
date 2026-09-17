
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

async function ObtenerLocalidades()
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    try {

        const res = await fetch(`${linkApi}/localidad`, {
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
        const tablaVacia = document.getElementById("mensajeTablaVacia")
        tablaVacia.innerHTML = ""

        if(resLocalidades == 0)
        {
            tablaVacia.innerHTML = "No hay localidades cargadas";
        }
        const tbodyLocalidades = document.getElementById("tbodyLocalidades")
        tbodyLocalidades.innerHTML = "";

        lcalidades.array.forEach(localidad => {

            const row = document.createElement("tr");
            row.classList.add("align-middle") 

            row.innerHTML = `
                <td>${localidad.nombreLocalidad}</td>
                <td>${localidad.nombreprovincia}</td>
            
            `
            
        });
    } catch (error) {
        
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

async function CrearLocalidad()
{
    /* const res = await fetch(`${linkApi}/localidad`, {
            method: "GET",
            headerrs: authHeaders(),
        }); */

        /* document.getElementById("modalLocalidad").show */
        /* const modalEl = document.getElementById("modalLocalidad");
        const modal = bootstrap.Modal.getInstance(modalEl); // agarra la instancia ya creada
        modal.show(); */
    
        /* const nombreLocalidad = document.getElementById("nombreLocalidad").value.trim();
        nombreLocalidad.toUpperCase();

        const provinciaId = document.getElementById("nombreLocalidad").value; */
    console.log("funciona guardar")


    try {
        
    } catch (error) {
        
    }
}



ObtenerProvincias();
ObtenerLocalidades();