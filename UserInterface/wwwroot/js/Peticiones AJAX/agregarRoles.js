$.get('/Rol/ListarPaginas', (data) => {
    var contenido = '';
    $.each(data, (key, item) => {
        contenido += '<tr>';
        contenido += '<td>' + item.nombrePagina + '</td>';
        contenido += '<td>';
        contenido += '<input type="checkbox" class="cbPagina" id="txtPag' + item.paginaId + '" name="pag' + item.paginaId + '" onchange="SeleccionarPagina()"/>';
        contenido += '</td>';
        contenido += '</tr>';
    });
    $('#tbPaginas').html(contenido);
    marcar();
})
function marcar() {
    if ($('#paginasArray').val() != '') {
        var texto = $('#paginasArray').val();
        var pagina = texto.split('$');
        for (var i = 0; i < pagina.length; i++) {
            var idGenerado = 'txtPag' + pagina[i];
            document.getElementById(idGenerado).checked = true;
        }
    }
}
function SeleccionarPagina() {
    var matriz = document.getElementsByClassName('cbPagina');
    var textoActual = '';
    for (var i = 0; i < matriz.length; i++) {
        var idgenerado = '#' + matriz[i].id;
        if ($(idgenerado).prop('checked')) {
            if (i >= matriz.length) {
                textoActual += matriz[i].name.replace('pag', '');
            } else {
                textoActual += matriz[i].name.replace('pag', '') + '$';
            }
        }
    }
    $('#paginasArray').val(textoActual);
}