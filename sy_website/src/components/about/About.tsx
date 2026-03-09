import "./About.css";

export default function About() {
  return (
    <section id="about" className="section about fade-in">
      <div className="about-title">
        <h2 className="gold">About</h2>
      </div>

      <div className="about-text">
        <p>
          Professional photographer specializing in emotional storytelling,
          capturing authentic moments and timeless memories in Curitiba. I
          document connections, light, and details with sensitivity and a
          cinematic eye. My work focuses on candid emotion, thoughtful
          composition, and creating heirloom images that feel both intimate and
          timeless. Available for editorials, portraits, and wedding
          storytelling.
        </p>
      </div>
      <div className="about-images" aria-hidden>
        <div className="img-stack">
          <img
            src="https://images.unsplash.com/photo-1519681393784-d120267933ba?q=80&w=1200&auto=format&fit=crop&ixlib=rb-4.0.3&s=1a"
            alt="portrait 1"
          />
          <img
            src="https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?q=80&w=1200&auto=format&fit=crop&ixlib=rb-4.0.3&s=2b"
            alt="portrait 2"
          />
        </div>
        <div className="img-tall">
          <img
            src="https://images.unsplash.com/photo-1524504388940-b1c1722653e1?q=80&w=1200&auto=format&fit=crop&ixlib=rb-4.0.3&s=3c"
            alt="portrait 3"
          />
        </div>
      </div>
    </section>
  );
}