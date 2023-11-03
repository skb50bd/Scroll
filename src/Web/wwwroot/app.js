document.querySelectorAll('.add-to-favorite').forEach(button => {
    button.addEventListener('click', function() {
        let favCount = this.parentElement.querySelector('.favorite-count');
        let currentCount = parseInt(favCount.textContent);
        favCount.textContent = currentCount + 1;

        // Add a tiny scale animation for feedback
        favCount.style.transform = 'scale(1.5)';
        setTimeout(() => {
            favCount.style.transform = 'scale(1)';
        }, 300);
    });
});

document.querySelectorAll('.category-card').forEach(card => {
    card.addEventListener('mouseover', function() {
        this.style.transform = 'scale(1.05)';
    });
    card.addEventListener('mouseout', function() {
        this.style.transform = 'scale(1)';
    });
});

document.querySelectorAll('.check-out-btn').forEach(button => {
    button.addEventListener('click', function() {
        alert('Are you sure you want to check out this product?');
    });
});
