window.onload = () =>
{
    llamarTipoUsuario();
}
function llamarTipoUsuario() {
    $.get('/Usuario/ListarTipoUsuario', (data) => {
        var contenido = '';
        contenido += '<option value="">--Seleccione una opción--</option>';
        $.each(data, (key, item) => {
            contenido += '<option value="' + item.tipoUsuarioId + '">' + item.nombreTipoUsuario + ': ' + item.descripcionTipoUsuario + '</option>';
        });
        $('#TipoUsuarioId').html(contenido);
        var id = $('#TipoUsuarioId').val();
        if (id != null && id != "")
            $('#TipoUsuarioId').val(id);
    });
}