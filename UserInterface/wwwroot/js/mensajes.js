mensaje();
function mensaje() {
    Swal.fire({
        position: 'center',
        icon: document.getElementById('txtIconoServer').value,
        title: document.getElementById('txtMensajeServidor').value,
        showConfirmButton: false,
        timer: 3000
    })
}