async function ObtenerEspecialidad()
{
    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    try {
        const res = await fetch(`${linkApi}/especialidades`, {
            method: "GET",
            headers: authHeaders(),

        });

        if(!res.ok)
        {
            Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Error al obtener las provincias",
            });
        }

        const especialidades = await res.json();
        console.log(especialidades)

        LimpiarModalEspecialidad();
        $("#modalEspecialidad").modal("hide");

        if(especialidades.length === 0)
        {
            document.getElementById("mensajeTablaVacia").innerHTML = "No hay especialidades cargadas"
        };

        const tbodyTablaEspecialidad = document.getElementById("tbodyEspecialidades");
        tbodyTablaEspecialidad.innerHTML = ""

        especialidades.forEach(especialidad => {
            const row= document.createElement("tr");
            row.classList.add("align-middle") 

            row.innerHTML = `
                <td>${especialidad.nombreEspecialidad}</td>
                <td>
                    <button class="btn btn-primary" onclick="AbrirModalEditar(${especialidad.especialidadId})">Editar</button>
                </td>

                <td>
                    <button class="btn btn-primary" onclick="AbrirModalEditar(${especialidad.especialidadId})">Deshabilitar</button>
                </td>
            
            `
            tbodyTablaEspecialidad.appendChild(row);
        });

    } catch (error) {
        console.error(error);
    }
}


function CargarEditarEspecialidad()
{
  var id = document.getElementById("especialidadId").value;

  if (id == 0)
  {
    CrearEspecialidad();
  } else {
    EditarEspecialidad(id)
  }
}

async function CrearEspecialidad()
{
    console.log("cargarfunciona")

    const authHeaders = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`
    });

    document.getElementById("errorNombreEspecialidad").textContent = "";
    
    let nombreEspecialidad = document.getElementById("nombreEspecialidad").value.trim();

    let campoCompleto = true;

    if(!nombreEspecialidad)
    {
        document.getElementById("errorNombreEspecialidad").innerHTML = "Ingrese el nombre de la especialidad"
        campoCompleto = false
    }

    if(!campoCompleto)
    {
        return
    }

    const especialidad = 
    {
        NombreEspecialidad: nombreEspecialidad,
    }

    try {
        const result = await Swal.fire({
            title: "¿Desea guardar la especialidad?",
            showCancelButton: true,
            confirmButtonText: "Guardar",

        });

        if(result.isConfirmed)
        {
            const respuesta = await fetch(`${linkApi}/especialidades`, {
                method: "POST",
                headers: authHeaders(),
                body: JSON.stringify(especialidad)
            });

            if(!respuesta.ok)
            {
                throw new error(`Error del servidor: ${response.status}`);
            }

            await Swal.fire("!Especialidad guardada!", "", "success")
        }

        ObtenerEspecialidad();
    } catch (error) {
        Swal.fire("Error", "No se pudo guardar la localidad", "error");
    }
}

async function EditarEspecialidad(id)
{
    console.log("editar funciona:")
}

function LimpiarModalEspecialidad()
{
    document.getElementById("especialidadId").value = 0;
    document.getElementById("nombreEspecialidad").value = "";
}

document.querySelectorAll(".mayuscula").forEach(input => {
    input.addEventListener("input", function () {
        this.value = this.value.toUpperCase();
    });
});

ObtenerEspecialidad();