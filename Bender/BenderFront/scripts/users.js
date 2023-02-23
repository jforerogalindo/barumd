/*const swal = Swal.mixin({
    width: 400,
});

const swalResponse = Swal.mixin({
    width: 400,
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
});*/

function limpiarModalAgregar(){
    $("#identificacion").val("");
    $("#nombre").val("");
    $("#password").val("");
    $("#rol").val("");
}

function limpiarModalEditar(){
    $("#nombreEdit").val("");
    $("#passwordEdit").val("");
    $("#rolEdit").val("");
}

async function getRol(rolId) {
    var roles = await rolsGetAll();
    for (const key in roles) {
        if (rolId === roles[key].rolId) {
            //console.log(roles[key].rolName)
            return roles[key].rolName;
        }
    }
}

async function listaUsuarios() {
    var response = await userGetAll();
    for (const key in response) {
        var newRowContent =
            '<tr><td scope="row">' +
            response[key].identification +
            "</td><td>" +
            response[key].name +
            "</td><td>" +
            (await getRol(response[key].rolId)) +
            '</td><td class="text-center"><a class="text-warning" onclick="cargaEditarUsuario('+response[key].identification +
            ')" data-bs-toggle="modal" data-bs-target="#modalEditar"><i class="fa-regular fa-pen-to-square"></i></a></td><td class="text-center"><a class="text-danger" onclick="eliminarUsuario('+response[key].identification +
            ')"><i class="fa-regular fa-trash"></i></a></td></tr>';
        $("#userTableBody").append(newRowContent);
    }
}

async function listaRoles() {
    var response = await rolsGetAll();
    for (const key in response) {
        var newOptionRol =
            '<option value="' +
            response[key].rolId +
            '">' +
            response[key].rolName +
            "</option>";
        $("#rol").append(newOptionRol);
        $("#rolEdit").append(newOptionRol);
    }
}

function eliminarUsuario(userId) {
    swal.fire({
        text: "¿Estas seguro de eliminar al usuario " + userId + "?",
        icon: 'warning',
        showDenyButton: true,
        confirmButtonText: "Confirmar",
        denyButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            eliminarUser(userId)
        } else if (result.isDenied) {
            swalResponse.fire({
                text: "Cancelado",
                icon: "error",
            });
        }
    });
}

async function eliminarUser(userId){
    try {
        var response = await deleteUser(userId);
    } catch (e) {
        swalResponse.fire({
            text: "Error al eliminar el usuario, por favor reintenta más tarde",
            icon: "error",
        });
        return;
    }
	if (response.success) {
		swalResponse.fire({
			text: "Eliminado!",
			icon: "success",
		});
		$("#userTableBody tr").remove();
		//listaUsuarios();
        $("#contenido").load("pages/usuario.html");
	} else {
		swalResponse.fire({
			text: "Error al eliminar el usuario, por favor reintenta más tarde",
			icon: "error",
		});
	}
}

function cargaEditarUsuario(identification){
    $("#editar").click(function(){
        editarUser(identification);
        $("#spinnerEditar").show();
        limpiarModalEditar();
    });
}

async function editarUser(identification){
    var nombre = $("#nombreEdit").val();
    var password = $("#passwordEdit").val();
    var rolId = $("#rolEdit").val();
    rolId = rolId == null || rolId == undefined || rolId == "" ? 0 : rolId;
    try {
        var response = await editUser(identification, nombre, password, rolId);
    } catch (e) {
        $("#spinnerEditar").hide();
        $("#cancelarEditar").click();
        swalResponse.fire({
            text: "Error al editar el usuario, por favor reintenta más tarde",
            icon: "error",
        });
        return;
    }
	if (response.success) {
        $("#spinnerEditar").hide();
        $("#cancelarEditar").click();
		swalResponse.fire({
			text: "Usuario editado!",
			icon: "success",
		});
		$("#userTableBody tr").remove();
		//listaUsuarios();
        $("#contenido").load("pages/usuario.html");
	} else {
        $("#spinnerEditar").hide();
        $("#cancelarEditar").click();
		swalResponse.fire({
			text: "Error al editar el usuario, por favor reintenta más tarde",
			icon: "error",
		});
	}
}

async function agregarUsuario(identification,nombre,password,rol){
    idbranch = 1101;
    try {
        var response = await insertUser(identification, nombre, password, rol, idbranch);
    } catch (e) {
        $("#spinnerAgregar").hide();
        $("#cancelarAgregar").click();
        swalResponse.fire({
            text: "Error al agregar el usuario, por favor reintenta más tarde",
            icon: "error",
        });
        return;
    }
    if (response.success) {
        $("#spinnerAgregar").hide();
        $("#cancelarAgregar").click();
        swalResponse.fire({
            text: "Agregado!",
            icon: "success",
        });
        //$("#userTableBody tr").remove();
        //listaUsuarios();
        $("#contenido").load("pages/usuario.html");
    } else {
        $("#spinnerAgregar").hide();
        $("#cancelarAgregar").click();
        swalResponse.fire({
            text: "Error al agregar el usuario, por favor reintenta más tarde",
            icon: "error",
        });
    }
}