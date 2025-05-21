import FeaturedGames from "@/components/FeaturedGames/FeaturedGames";
import HowItWorks from "@/components/HowItWorks/HowItWorks";

export default function Home() {
  return (
    <div className="main-page">
      <div className="welcome-div">
        <h1>Find your gaming buddies</h1>
        <p>Connect with other players and enjoy your favourite games together</p>
        <input type="text" placeholder="Search for games" />
      </div>
      <FeaturedGames />
      <HowItWorks />
    </div>
  );
}
