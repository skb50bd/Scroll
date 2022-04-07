document.addEventListener(
    'DOMContentLoaded',
    _ => {
        const tooltipTriggerList =
            [].slice.call(
                document.querySelectorAll(
                    '[data-bs-toggle="tooltip"]'))

        const tooltipList =
            tooltipTriggerList.map(tooltipTriggerEl =>
                new bootstrap.Tooltip(tooltipTriggerEl));

        const jcImagesTooltipInstance =
            JcImagesTooltip.create(
                '.hover-image',
                {
                    width: 128,
                    height: 128
                });

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
        
        const existingImageDiv = 
            document.querySelector("#existing-image");
        
        const newImageDiv = 
            document.querySelector("#new-image");
        
        const newImageNameInput = 
            document.querySelector("#EditModel_Picture_Name");

        const existingImageNameInput =
            document.querySelector("#EditModel_Product_ImageName");

        document
            .querySelector('#use-existing-image-switch')
            .addEventListener(
                'click',
                event => {
                    if (event.target.checked) {
                        existingImageDiv.removeAttribute('hidden');
                        newImageDiv.setAttribute('hidden', 'hidden');
                        newImageNameInput.value = 'no-image';
                    }
                    else {
                        newImageDiv.removeAttribute('hidden');
                        existingImageDiv.setAttribute('hidden', 'hidden');
                        newImageNameInput.value = '';
                    }
                })
    });