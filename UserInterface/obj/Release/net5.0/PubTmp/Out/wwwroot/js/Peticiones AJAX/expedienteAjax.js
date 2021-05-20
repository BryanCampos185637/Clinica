function EliminarRegistro(id) {
    confirmarEliminacion('el paciente', () => {
        $.get('/Expediente/Eliminar?id=' + id, (rpt) => {
            if (rpt == 'Exito') {
                alert('Se elimino exitosamente');
            }
            else {
                alert(rpt);
            }
        });
    });
}