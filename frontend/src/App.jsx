import { useState, useEffect } from 'react';
import { Shield, User, Star, Hash } from 'lucide-react';

function App() {
  const [assets, setAssets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchAssets = async () => {
      try {
        const response = await fetch('http://localhost:7071/api/getassetsbyplayer');
        if (!response.ok) {
          throw new Error(`API Error: ${response.status}`);
        }
        const data = await response.json();
        setAssets(data);
        setError(null);
      } catch (err) {
        console.error("Fetch error:", err);
        setError("Failed to load player assets. Ensure the Azure Function is running locally.");
      } finally {
        setLoading(false);
      }
    };

    fetchAssets();
  }, []);

  return (
    <div className="app-container">
      <header className="header">
        <h1 className="title">BATTLEGAME</h1>
        <p className="subtitle">Player Assets Directory</p>
      </header>

      <div className="table-container">
        {loading ? (
          <div className="loading-state">
            <div className="loading-spinner"></div>
            <p>Gathering intel from the servers...</p>
          </div>
        ) : error ? (
          <div className="error-state">
            <Shield size={48} color="#ef4444" style={{ margin: '0 auto 1rem' }} />
            <p style={{ color: '#ef4444' }}>{error}</p>
          </div>
        ) : assets.length === 0 ? (
          <div className="empty-state">
            <User size={48} color="#94a3b8" style={{ margin: '0 auto 1rem' }} />
            <p>No players or assets found in the database.</p>
          </div>
        ) : (
          <table className="assets-table">
            <thead>
              <tr>
                <th style={{ width: '80px', textAlign: 'center' }}><Hash size={16} /></th>
                <th>Player Information</th>
                <th style={{ textAlign: 'center' }}>Level</th>
                <th style={{ textAlign: 'center' }}>Age</th>
                <th>Asset Designation</th>
              </tr>
            </thead>
            <tbody>
              {assets.map((item, index) => (
                <tr key={`${item.playerName || 'Unknown'}-${item.assetName || 'Unknown'}-${index}`}>
                  <td style={{ textAlign: 'center', color: 'var(--text-muted)' }}>
                    {String(index + 1).padStart(2, '0')}
                  </td>
                  <td>
                    <div className="player-name">
                      <div className="avatar">
                        {(item.playerName && item.playerName.length > 0) ? item.playerName.charAt(0).toUpperCase() : '?'}
                      </div>
                      {item.playerName || 'Unknown Player'}
                    </div>
                  </td>
                  <td style={{ textAlign: 'center' }}>
                    <span className="badge badge-level">
                      <Star size={12} style={{ marginRight: '4px' }} />
                      Lvl {item.level}
                    </span>
                  </td>
                  <td style={{ textAlign: 'center', color: 'var(--text-muted)' }}>
                    {item.age}
                  </td>
                  <td>
                    <span className="badge badge-asset">
                      <Shield size={12} style={{ marginRight: '4px' }} />
                      {item.assetName}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}

export default App;
