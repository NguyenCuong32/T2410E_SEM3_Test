import type { PlayerAsset } from "@/types";

async function getPlayerAssets(): Promise<PlayerAsset[]> {
  const API_BASE =
    process.env.NEXT_PUBLIC_API_BASE ?? "http://localhost:7071/api";

  const res = await fetch(`${API_BASE}/getassetsbyplayer`, {
    cache: "no-store",
  });

  if (!res.ok) {
    throw new Error(`Failed to fetch data: ${res.status} ${res.statusText}`);
  }

  return res.json();
}

export default async function HomePage() {
  let data: PlayerAsset[] = [];
  let errorMessage: string | null = null;

  try {
    data = await getPlayerAssets();
  } catch (err) {
    errorMessage = err instanceof Error ? err.message : "Unknown error";
  }

  return (
    <main className="container">
      <div className="header">
        <h1>BattleGame — Player Asset Report</h1>
        <p>Overview of all players and their assigned assets.</p>
      </div>

      {errorMessage ? (
        <div className="error-box">
          <strong>Could not load data:</strong> {errorMessage}
          <br />
          <small>
            Make sure the Azure Functions host is running on port 7071.
          </small>
        </div>
      ) : (
        <div className="card">
          <div className="table-wrapper">
            {data.length === 0 ? (
              <p className="empty">No records found.</p>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th className="no-col">No</th>
                    <th>Player Name</th>
                    <th>Level</th>
                    <th>Age</th>
                    <th>Asset Name</th>
                  </tr>
                </thead>
                <tbody>
                  {data.map((row, index) => (
                    <tr key={index}>
                      <td className="no-col">{index + 1}</td>
                      <td>{row.playerName ?? "—"}</td>
                      <td>
                        <span className="badge">{row.level ?? "—"}</span>
                      </td>
                      <td>{row.age ?? "—"}</td>
                      <td>{row.assetName ?? "—"}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </div>
        </div>
      )}
    </main>
  );
}
