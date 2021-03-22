$(document).ready(() => {

    $('#saveChanges').click((e) => {

        const objClient = {
            id: $('#clientID').val(),
            nombre: $('#firstName').val(),
            apellido: $('#lastName').val(),
            telefono: $('#phone').val(),
        };

        Swal.fire({
            title: 'Are you sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, do it!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: '/Home/SaveChangesClient',
                    method: 'POST',
                    data: { model: objClient },
                    success: (res) => {
                        const { status } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Client Update',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(() => {
                                document.location.href = `/Home/UserDetails/${$('#clientID').val()}`;
                            }, 1500)
                        }

                        if (status == 400) {                            
                            const { message } = res;
                            toastr.options = {
                                "closeButton": true,
                                "positionClass": "toast-top-right"
                            }
                            for (var i = 0; i < message.length; i++) {
                                toastr["error"](`<h3>${message[i]}</h3>`)
                            }
                        }
                     

                    },
                    error: (err) => {
                        console.error(err.responseText);
                    }
                });

            }
        });

    });


});