document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', function (event) {
            event.preventDefault();
            const formData = new FormData(loginForm);
            const data = {
                Username: formData.get('username'),
                Password: formData.get('password')
            };
            fetch('/Account/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(result => {
                    if (result.token) {
                        localStorage.setItem('token', result.token);
                        window.location.href = '/';
                    } else {
                        alert('Login fallito');
                    }
                })
                .catch(error => {
                    console.error('There was a problem with the fetch operation:', error);
                    alert('Errore durante il login: ' + error.message);
                });
        });
    }
});
