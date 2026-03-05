import React, { useState, useEffect } from 'react';
import './App.css';

function App() {
  const [reportData, setReportData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Thay bằng URL Azure Function thật sau khi deploy (thêm ?code=yourkey nếu dùng Function Key)
        const response = await fetch('https://your-function-app-name.azurewebsites.net/api/getassetsbyplayer');
        if (!response.ok) {
          throw new Error('Failed to fetch data');
        }
        const data = await response.json();

        // Thêm No nếu API chưa có
        const dataWithNo = data.map((item, index) => ({
          No: index + 1,
          ...item,
        }));

        setReportData(dataWithNo);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) return <p>Loading report...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div className="App">
      <header className="App-header">
        <h1>Player Assets Report (BattleGame)</h1>
      </header>
      <table>
        <thead>
          <tr>
            <th>No</th>
            <th>Player name</th>
            <th>Level</th>
            <th>Age</th>
            <th>Asset name</th>
          </tr>
        </thead>
        <tbody>
          {reportData.map((item) => (
            <tr key={item.No}>
              <td>{item.No}</td>
              <td>{item.PlayerName}</td>
              <td>{item.Level}</td>
              <td>{item.Age}</td>
              <td>{item.AssetName}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;