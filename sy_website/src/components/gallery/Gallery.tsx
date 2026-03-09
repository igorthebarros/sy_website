import { PhotoProvider, PhotoView } from "react-photo-view";
import { useEffect, useState } from "react";
import "react-photo-view/dist/react-photo-view.css";
import "./Gallery.css";
import type { InstagramPost } from "../../models/InstagramPost";
import { getInstagramPosts } from "../../services/InstagramService";

export default function Gallery() {
  const [posts, setPosts] = useState<InstagramPost[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // TODO: Move API call to a service file and handle errors properly
  // useEffect(() => {
  //   fetch("https://localhost:32771/instagram/posts")
  //     .then(res => res.json())
  //     .then(data => {
  //       // Meta payload usually inside "data"
  //       setPosts(data.data || data);
  //       setLoading(false);
  //     })
  //     .catch(() => setLoading(false));
  // }, []);

  useEffect(() => {
    async function loadPosts() {
      try {
        const data = await getInstagramPosts();
        setPosts(data);
      } catch (err) {
        console.error(err);
        setError("Unable to load gallery at the moment.");
      } finally {
        setLoading(false);
      }
    }
    loadPosts();
  }, []);

  if (loading) {
    return (
      <section className="section">
        <h2 className="gold">Gallery</h2>
        <p className="center">Loading photos...</p>
      </section>
    );
  }

  if (error) {
    return (
      <section className="section">
        <h2 className="gold">Gallery</h2>
        <p className="center error">{error}</p>
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
                  {post.caption && (
                    <div className="caption">
                      {post.caption.slice(0, 60)}
                    </div>
                  )}
                </a>
              </PhotoView>
            ))}
        </div>
      </PhotoProvider>
    </section>
  );
}