document.addEventListener(
    'DOMContentLoaded',
    _ => {
        document
            .querySelectorAll('.js-delete-action')
            .forEach(elem => {
                elem.addEventListener(
                    'click',
                    event => {
                        const deleteActionUrl =
                            event.target.getAttribute('data-delete-url');
                        
                        const redirectUrl = 
                            event.target.getAttribute('data-redirect-url');
                        
                        const isDeletionConfirmed = 
                            confirm("Are you sure want to delete this item?");
                        
                        if (isDeletionConfirmed) {
                            fetch(deleteActionUrl, {method: 'DELETE'})
                                .then(_ => window.location = redirectUrl);
                        }
                    })
            });
    });