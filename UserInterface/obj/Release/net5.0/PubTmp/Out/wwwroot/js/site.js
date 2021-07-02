mensaje();
function mensaje() {
    Swal.fire({
        position: 'center',
        icon: document.getElementById('txtIconoServer').value,
        title: document.getElementById('txtMensajeServidor').value,
        showConfirmButton: false,
        timer: 1500
    })
}
function LimpiarFormularioBusqueda() {
    document.getElementById('filtro').value = '';
    document.getElementById('frmFiltroBusqueda').submit();
}
function CerrarSesion() {
    Swal.fire({
        title: 'Cerrar sesión',
        text: '¿Estas seguro que deseas cerrar sesión?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si! cerrar',
        cancelButtonText:'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('btnCerrarSesion').click();
        }
    })
}
function confirmarEliminacion(complemento = 'el registro', callback) {
    Swal.fire({
        title: 'Eliminar',
        text: '¿Estas seguro que deseas eliminar ' + complemento + '?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si! Eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    })
}
function EliminarRegistro(id) {
    confirmarEliminacion('el registro', () => {
        document.getElementById('id').value = id;
        document.getElementById('frmEliminar').submit();
    });
}
