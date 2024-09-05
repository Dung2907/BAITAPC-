import axios from "axios";
import { useEffect, useState } from 'react';
import { API_URL, baseURL } from '../constants/base';

function Admin() {
  document.title = "Admin";
  const [partners, setPartners] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    axios.get(`${baseURL}${API_URL}/admin`, {
      withCredentials: true,
    })
    .then(response => response.data)
    .then(data => {
      setPartners(data.trustedPartners);
      setLoading(false);
    })
    .catch(error => {
      console.log("Error fetching partners: ", error);
      setError('Failed to load partners. Please try again later.');
      setLoading(false);
    });
  }, []);

  return (
    <section className='admin-page page'>
      <header>
        <h1>Admin Page</h1>
      </header>
      <section>
        {loading ? (
          <div className='loading'>Loading...</div>
        ) : (error ? (
          <div className='error-message'>{error}</div>
        ) : partners.length > 0 ? (
          <div>
            <h2>Our Trusted Partners:</h2>
            <ul className='partner-list'>
              {partners.map((partner, i) => (
                <li key={i} className='partner-item'>{partner}</li>
              ))}
            </ul>
          </div>
        ) : (
          <div className='no-partners'>No trusted partners available.</div>
        ))}
      </section>
    </section>
  );
}

export default Admin;
