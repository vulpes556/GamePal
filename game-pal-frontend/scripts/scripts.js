export async function fetchUserGames() {
    const response = await fetch("/api/user-games", {
        method: "GET",
        headers: {},
    });

    if (!response.ok) {
        throw new Error("Something went wrong");
    }
    const result = await response.json();
    return result;
};


export async function registerUser(registrationData) {
    const response = await fetch("/api/user/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(registrationData)
    });

    if (!response.ok) {
        const errorData = await response.json();
        console.error("Registration failed:", errorData);
        throw new Error("Error during registration", errorData)
    }
    const data = await response.json();
    return data;
}
