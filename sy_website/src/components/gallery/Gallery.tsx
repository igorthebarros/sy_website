import { PhotoProvider, PhotoView } from "react-photo-view";
import { useEffect, useState } from "react";
import "react-photo-view/dist/react-photo-view.css";
import "./Gallery.css";
import type { InstagramPost } from "../../models/InstagramPost";

export default function Gallery() {
  const [posts, setPosts] = useState<InstagramPost[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("https://localhost:32771/instagram/posts")
      .then(res => res.json())
      .then(data => {
        // Meta payload usually inside "data"
        setPosts(data.data || data);
        setLoading(false);
      })
      .catch(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <section className="section">
        <h2 className="gold">Gallery</h2>
        <p style={{ textAlign: "center" }}>Loading photos...</p>
      </section>
    );
  }

  return (
    <section id="gallery" className="section fade-in">
      <h2 className="gold">Latest Work</h2>

      <PhotoProvider>
        <div className="gallery-grid">
          {posts
            .filter(p => p.media_type !== "VIDEO") // optional
            .slice(0, 12)
            .map(post => (
              <PhotoView key={post.id} src={post.media_url}>
                <a href={post.permalink} target="_blank" rel="noreferrer">

                  <img
                    src={post.media_url}
                    alt={post.caption || "Instagram photo"}
                    loading="lazy"
                  />

                  <div className="caption">
                    {post.caption?.slice(0, 60)}
                  </div>
                </a>
              </PhotoView>
            ))}
        </div>
      </PhotoProvider>
    </section>
  );
}