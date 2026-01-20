const formContainer = document.getElementById('form-container');
const list = document.getElementById('list');
const message = document.getElementById('message');

function setMessage(text, isError) {
  message.innerHTML = text ? `<p class="${isError ? 'error' : 'info'}">${text}</p>` : '';
}

async function fetchJson(path, opts) {
  const res = await fetch(path, opts);
  const text = await res.text();
  let json = null;
  try { json = text ? JSON.parse(text) : null; } catch {}
  if (!res.ok) throw { status: res.status, statusText: res.statusText, body: json || text };
  return json;
}

async function loadComicBooks() {
  setMessage('Loading comic books...');
  formContainer.innerHTML = `
    <h3>Add Comic Book</h3>
    <form id="comic-form">
      <input name="title" placeholder="Title" required />
      <input name="author" placeholder="Author" />
      <input name="pricePerDay" placeholder="Price per day" type="number" step="0.01" required />
      <button type="submit">Add</button>
    </form>
    <hr />`;

  document.getElementById('comic-form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const fd = new FormData(e.target);
    const body = { Title: fd.get('title'), Author: fd.get('author'), PricePerDay: parseFloat(fd.get('pricePerDay')) };
    try {
      const created = await fetchJson('/api/comicbooks', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body) });
      setMessage('Comic created: ' + (created?.Title || created?.title || created?.comicBookID || '' ));
      await renderComicList();
    } catch (err) {
      setMessage('Create failed: ' + (err.body?.message || JSON.stringify(err.body) || err.statusText), true);
    }
  });

  await renderComicList();
}

async function renderComicList() {
  try {
    const data = await fetchJson('/api/comicbooks');
    setMessage('');
    if (!Array.isArray(data) || data.length === 0) {
      list.innerHTML = '<p>No comic books found.</p>';
      return;
    }
    list.innerHTML = `<h3>Comic Books</h3><ul class="items">${data.map(c => `
      <li><strong>#${c.comicBookID ?? c.ComicBookID}</strong> ${c.title ?? c.Title} — ${c.author ?? c.Author} — $${(c.pricePerDay ?? c.PricePerDay)?.toFixed?.(2) ?? c.pricePerDay}
        <button data-id="${c.comicBookID ?? c.ComicBookID}" class="delete">Delete</button>
      </li>`).join('')}</ul>`;

    list.querySelectorAll('.delete').forEach(btn => btn.addEventListener('click', async (e) => {
      const id = e.target.getAttribute('data-id');
      if (!confirm('Delete comic #' + id + '?')) return;
      try {
        await fetchJson('/api/comicbooks/' + id, { method: 'DELETE' });
        setMessage('Deleted comic #' + id);
        await renderComicList();
      } catch (err) {
        setMessage('Delete failed: ' + (err.body || err.statusText), true);
      }
    }));
  } catch (err) {
    list.innerHTML = `<p class="error">Load failed: ${err.statusText || JSON.stringify(err)}</p>`;
  }
}

function showCustomerRegister() {
  list.innerHTML = '';
  formContainer.innerHTML = `
    <h3>Register Customer</h3>
    <form id="customer-form">
      <input name="fullName" placeholder="Full name" required />
      <input name="phoneNumber" placeholder="Phone number" />
      <button type="submit">Register</button>
    </form>
    <div id="customer-result"></div>
  `;

  document.getElementById('customer-form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const fd = new FormData(e.target);
    const body = { FullName: fd.get('fullName'), PhoneNumber: fd.get('phoneNumber') };
    try {
      const res = await fetchJson('/api/customers/register', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body) });
      document.getElementById('customer-result').innerHTML = `<pre>${JSON.stringify(res, null, 2)}</pre>`;
      setMessage('Customer registered.');
    } catch (err) {
      setMessage('Register failed: ' + (err.body || err.statusText), true);
    }
  });
}

function showRentalForm() {
  formContainer.innerHTML = `
    <h3>Create Rental (JSON)</h3>
    <p>Use JSON with properties: CustomerID, RentalDate, ReturnDate, Items (array of { ComicBookID, Quantity })</p>
    <form id="rental-form">
      <textarea name="payload" rows="8" style="width:100%">{ "CustomerID": 1, "RentalDate": "2026-01-20T00:00:00", "ReturnDate": "2026-01-25T00:00:00", "Items": [{ "ComicBookID": 1, "Quantity": 1 }] }</textarea>
      <button type="submit">Create Rental</button>
    </form>
    <div id="rental-result"></div>
  `;

  document.getElementById('rental-form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const payload = e.target.payload.value;
    try {
      const obj = JSON.parse(payload);
      const res = await fetchJson('/api/rentals', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(obj) });
      document.getElementById('rental-result').innerHTML = `<pre>${JSON.stringify(res, null, 2)}</pre>`;
      setMessage('Rental created.');
    } catch (err) {
      setMessage('Create rental failed: ' + (err.body || err.statusText || err.message), true);
    }
  });
}

async function showReports() {
  setMessage('Loading reports...');
  formContainer.innerHTML = '';
  list.innerHTML = '<p>Loading reports...</p>';
  try {
    const data = await fetchJson('/api/reports');
    list.innerHTML = `<pre>${JSON.stringify(data, null, 2)}</pre>`;
    setMessage('');
  } catch (err) {
    list.innerHTML = `<p class="error">${err.statusText || JSON.stringify(err)}</p>`;
  }
}

document.getElementById('btn-comics').addEventListener('click', () => loadComicBooks());
document.getElementById('btn-customers').addEventListener('click', () => showCustomerRegister());
document.getElementById('btn-rentals').addEventListener('click', () => showRentalForm());
document.getElementById('btn-reports').addEventListener('click', () => showReports());

// initial load
loadComicBooks();
