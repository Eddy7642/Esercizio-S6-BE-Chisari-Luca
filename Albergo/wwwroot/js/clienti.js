document.addEventListener('DOMContentLoaded', function () {
    loadClienti();
});

function loadClienti() {
    const token = localStorage.getItem('token');
    fetch('/api/clienti', {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
        .then(response => response.json())
        .then(data => {
            let clientiTable = document.getElementById('clientiTable');
            clientiTable.innerHTML = '';
            data.forEach(cliente => {
                let row = clientiTable.insertRow();
                row.insertCell(0).textContent = cliente.codiceFiscale;
                row.insertCell(1).textContent = cliente.cognome;
                row.insertCell(2).textContent = cliente.nome;
                row.insertCell(3).textContent = cliente.città;
                row.insertCell(4).textContent = cliente.provincia;
                row.insertCell(5).textContent = cliente.email;
                row.insertCell(6).textContent = cliente.telefono;
                row.insertCell(7).textContent = cliente.cellulare;
                let actionsCell = row.insertCell(8);
                actionsCell.innerHTML = `
                <a href="/clienti/details/${cliente.codiceFiscale}">Dettagli</a> |
                <a href="/clienti/edit/${cliente.codiceFiscale}">Modifica</a> |
                <a href="#" onclick="deleteCliente('${cliente.codiceFiscale}')">Elimina</a>
            `;
            });
        });
}

function deleteCliente(codiceFiscale) {
    if (confirm('Sei sicuro di voler eliminare questo cliente?')) {
        const token = localStorage.getItem('token');
        fetch(`/api/clienti/${codiceFiscale}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
            .then(response => {
                if (response.ok) {
                    loadClienti();
                } else {
                    alert('Errore durante l\'eliminazione del cliente');
                }
            });
    }
}
