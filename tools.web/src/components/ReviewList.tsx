import type { ReviewDto } from '../api/reviewApi';

interface ReviewListProps {
    reviews?: ReviewDto[];
}

export default function ReviewList({ reviews }: ReviewListProps) {
    if (!reviews || reviews.length === 0) return <div>No reviews yet.</div>;
    return (
        <ul style={{ paddingLeft: 0, listStyle: 'none' }}>
            {reviews.map((review) => (
                <li key={review.id} style={{ marginBottom: '1rem', borderBottom: '1px solid #eee', paddingBottom: '0.5rem' }}>
                    <div><strong>Rating:</strong> {review.rating} ⭐</div>
                    <div><strong>Comment:</strong> {review.comment || 'No comment'}</div>
                    <div><strong>Reviewer:</strong> {review.reviewerName || 'Anonymous'}</div>
                    <div style={{ fontSize: '0.9em', color: '#888' }}>{new Date(review.createdAt).toLocaleString()}</div>
                </li>
            ))}
        </ul>
    );
}
