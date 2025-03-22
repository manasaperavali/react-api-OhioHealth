import { useState } from "react";
import axios from "axios";
import "./App.css";

const API_URL = "http://localhost:5250/api/user";
const API_KEY = "b48b3caf-ce03-424c-bb75-19...";

function App() {
    const [formData, setFormData] = useState({
        FirstName: "",
        CityName: "",
        YearOfJoining: "",
    });

    const [retrievedData, setRetrievedData] = useState([]);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSave = async () => {
        console.log("Sending payload:", formData);

        const year = parseInt(formData.YearOfJoining, 10);
        const thisYear = new Date().getFullYear();

        if (year < thisYear - 5 || year > thisYear) {
            alert(`Year Of Joining must be between ${thisYear - 5} and ${thisYear}`);
            return;
        }

        try {
            await axios.post(API_URL, formData, {
                headers: {
                    "Content-Type": "application/json",
                    "x-api-key": API_KEY,
                },
            });
            alert("Data saved successfully!");
        } catch (error) {
            console.error("Error saving data:", error.response?.data || error.message);
        }
    };

    const handleRetrieve = async () => {
        try {
            const queryParams = new URLSearchParams();
            if (formData.FirstName) queryParams.append("firstName", formData.FirstName);
            if (formData.CityName) queryParams.append("cityName", formData.CityName);
            if (formData.YearOfJoining) queryParams.append("yearOfJoining", formData.YearOfJoining);

            const url = queryParams.toString()
                ? `${API_URL}/filter?${queryParams.toString()}`
                : API_URL;

            const response = await axios.get(url, {
                headers: { "x-api-key": API_KEY },
            });
            console.log("Retrieved data:", response.data);
            setRetrievedData(response.data);
        } catch (error) {
            console.error(" Error retrieving data", error);
        }
    };

    return (
        <div className="outer-container">
            <div className="page-container">
                <div className="form-box">
                    <h1>First Page</h1>
                    <div className="input-group">
                        <label>First Name:</label>
                        <input
                            type="text"
                            name="FirstName"
                            onChange={handleChange}
                            className="formatted-input full-width"
                        />
                    </div>
                    <div className="input-group">
                        <label>City Name:</label>
                        <input
                            type="text"
                            name="CityName"
                            onChange={handleChange}
                            className="formatted-input full-width"
                        />
                    </div>
                    <div className="input-group">
                        <label>Year of Joining:</label>
                        <input
                            type="number"
                            name="YearOfJoining"
                            onChange={handleChange}
                            min={new Date().getFullYear() - 5}
                            max={new Date().getFullYear()}
                            className="formatted-input full-width"
                        />
                    </div>

                    <div className="button-group">
                        <button className="button full-width" onClick={handleSave}>Save</button>
                        <button className="button full-width" onClick={handleRetrieve}>Retrieve</button>
                    </div>

                    <div>
                        <h2>Retrieved Data:</h2>
                        <ul>
                            {retrievedData.map((user, index) => (
                                <li key={index}>
                                    {user.firstName || "-"} - {user.cityName || "-"} - {user.yearOfJoining || "-"}
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default App;
