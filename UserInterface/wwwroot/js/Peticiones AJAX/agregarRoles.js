$.get("/Rol/ListarPaginas", (data) => {
  let contenido = "";
  contenido += "<tr>";
  contenido += "<td>SELECCIONAR TODAS</td>";
  contenido += "<td>";
  contenido +=
    '<input type="checkbox" id="cbxSeleccionarTodas" onclick="SeleccionarTodasLasPaginas()" />';
  contenido += "</td>";
  contenido += "</tr>";
  $.each(data, (key, item) => {
    contenido += "<tr>";
    contenido += "<td>" + item.nombrePagina + "</td>";
    contenido += "<td>";
    contenido +='<input type="checkbox" class="cbPagina" id="txtPag' +item.paginaId +'" name="pag' +item.paginaId +'" onchange="SeleccionarPagina()"/>';
    contenido += "</td>";
    contenido += "</tr>";
  });
  $("#tbPaginas").html(contenido);
  marcar();
});
function marcar() {
  if ($("#paginasArray").val() != "") {
    let texto = $("#paginasArray").val();
    let pagina = texto.split("$");
    for (let i = 0; i < pagina.length; i++) {
      let idGenerado = "txtPag" + pagina[i];
      document.getElementById(idGenerado).checked = true;
    }
  }
}
function SeleccionarPagina() {
  let matriz = $(".cbPagina");
  let textoActual = "";
  for (let i = 0; i < matriz.length; i++) {
    let idgenerado = "#" + matriz[i].id;
    if ($(idgenerado).prop("checked")) {
      if (i >= matriz.length) {
        textoActual += matriz[i].name.replace("pag", "");
      } else {
        textoActual += matriz[i].name.replace("pag", "") + "$";
      }
    }
  }
  $("#paginasArray").val(textoActual);
}
function SeleccionarTodasLasPaginas() {
  try {
    let matriz = $(".cbPagina");
    let estadoCbx = $("#cbxSeleccionarTodas").prop("checked");
    for (let i = 0; i < matriz.length; i++) {
      if (estadoCbx) {
        matriz[i].checked = true;
      } else {
        matriz[i].checked = false;
      }
    }
    SeleccionarPagina();
  } catch (e) {
    alert("Error al intentar marcar todas las paginas");
  }
}
