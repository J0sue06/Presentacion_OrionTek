$(document).ready(() => {

    $('#logOut').click((e) => {
        e.preventDefault();

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
                    url: '/Login/LogOut',
                    success: (res) => {
                        document.location.href = '/Login';
                    },
                    error: (err) => {
                        console.error(err.responseText);
                    }
                });
            }
        })

      

    });

});