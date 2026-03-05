import { useEffect, useState } from "react";
import { getAssetsByPlayer } from "../api";

function AssetTable() {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const res = await getAssetsByPlayer();
            setData(res.data);
        } catch (error) {
            console.error("Error fetching data:", error);
        } finally {
            setLoading(false);
        }
    };

    if (loading)
        return <div className="text-center mt-5">Loading data...</div>;

    if (data.length === 0)
        return <div className="text-center mt-5">No data available</div>;

    return (
        <div className="container mt-5">
            <h2 className="text-center mb-4">Assets Report</h2>
            <table className="table table-bordered table-striped text-center">
                <thead className="table-dark">
                    <tr>
                        <th>No</th>
                        <th>Player Name</th>
                        <th>Level</th>
                        <th>Age</th>
                        <th>Asset Name</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((item, index) => (
                        <tr key={index}>
                            <td>{item.no}</td>
                            <td>{item.playerName}</td>
                            <td>{item.level}</td>
                            <td>{item.age}</td>
                            <td>{item.assetName}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default AssetTable;