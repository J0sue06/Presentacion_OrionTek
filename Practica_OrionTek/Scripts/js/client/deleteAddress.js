$(document).ready(() => {
    

    $(document).on("click", ".btn_delete", function (evt) {
        evt.preventDefault();
        const idAddress = $(this).data("id");

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
                    url: '/Home/DeleteAddress',
                    method: 'POST',
                    data: { idAddress: idAddress },
                    success: (res) => {
                        console.log(res);
                        const { status } = res;
                        if (status == 200) {

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Deleted',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(() => {
                                document.location.reload();
                            }, 1500)

                        }
                        if (status == 400) {
                            const { message } = res;
                            console.log(message);
                        }

                    },
                    error: (err) => {
                        console.error(err.responseText);
                    }
                });
            }

        });

    });

    $(document).on("click", "#deleteClient", function (evt) {
        evt.preventDefault();
        const idClient = $(this).data("id");

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
                    url: '/Home/DeleteClient',
                    method: 'POST',
                    data: { idClient: idClient },
                    success: (res) => {                        
                        const { status } = res;
                        if (status == 200) {

                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Client Deleted',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(() => {
                                document.location.href = "/Home";
                            }, 1500)

                        }
                        if (status == 400) {
                            const { message } = res;
                            console.log(message);
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