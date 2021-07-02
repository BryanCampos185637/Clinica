///jquery
$(document).ready(function () {
  AnclaActiva(window.location.href.replace("//", " ").split("/"));
  EjecutarMensajeServidor();
});
function EjecutarMensajeServidor() {
  var validacion = $("#txtIconoServer").val();
  if (validacion != undefined) mensaje();
}
function mensaje() {
  Swal.fire({
    position: "center",
    icon: $("#txtIconoServer").val(),
    title: $("#txtMensajeServidor").val(),
    showConfirmButton: false,
    timer: 2500,
  });
}
function LimpiarFormularioBusqueda() {
  $("#filtro").val("");
  $("#frmFiltroBusqueda").submit();
}
function CerrarSesion() {
  Swal.fire({
    title: "Cerrar sesión",
    text: "¿Estas seguro que deseas cerrar sesión?",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Si! cerrar",
    cancelButtonText: "Cancelar",
  }).then((result) => {
    if (result.isConfirmed) {
      document.getElementById("btnCerrarSesion").click();
    }
  });
}
function confirmarEliminacion(complemento = "el registro", callback) {
  Swal.fire({
    title: "Eliminar",
    text: "¿Estas seguro que deseas eliminar " + complemento + "?",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Si! Eliminar",
    cancelButtonText: "Cancelar",
  }).then((result) => {
    if (result.isConfirmed) {
      callback();
    }
  });
}
function EliminarRegistro(id) {
  confirmarEliminacion("el registro", () => {
    $("#id").val(id);
    $("#frmEliminar").submit();
  });
}

function AnclaActiva(raiz) {
  let opcionesMenu = $(".validate-active");
  for (let i = 0; i < opcionesMenu.length; i++) {
    let controlador = opcionesMenu[i].href.replace("//", " ").split("/");
    let pagina_encontrada = AsignarRemoverActive(
      opcionesMenu[i],
      controlador[1],
      raiz[1]
    ); //si retorno false entonces debe seguir la iteracion
    if (pagina_encontrada) break;
  }
}

function AsignarRemoverActive(tag, controlador, raiz) {
  let pagina_encontrada = false;
  if (controlador != "Home" && controlador != "Account") {
    if (controlador == RemoverParametrosUrl(raiz)) {
      tag.classList.add("activo");
      pagina_encontrada = true;
    } else {
      tag.classList.remove("activo");
    }
  }
  return pagina_encontrada;
}
function RemoverParametrosUrl(url) {
  let posicionDelSignoInterrogacion = 0; //para saber la posicion del ?
  if (url.includes("?")) {
    //si la url contiene un signo de interrogacion
    let arrayUrl = url.split(""); //convertimos el string en un array
    for (let i = 0; i < arrayUrl.length; i++) {
      //iteramos para saber la posicion del signo
      if (arrayUrl[i] == "?") {
        //si en la iteracion se encuentra un signo de pregunta
        break; //detenemos el bucle
      }
      posicionDelSignoInterrogacion++; //incrementamos 1
    }
    url = url.substring(0, posicionDelSignoInterrogacion); //extraemos desde la posicion 0 hasta la posicion donde se encuenta el signo
  }
  return url;
}
