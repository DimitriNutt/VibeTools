import { useState } from 'react';
import { createReview } from '../api/reviewApi';

interface ReviewFormProps {
    toolId: string;
    onReviewAdded: () => void;
}

export default function ReviewForm({ toolId, onReviewAdded }: ReviewFormProps) {
    const [reviewRating, setReviewRating] = useState(5);
    const [reviewComment, setReviewComment] = useState('');
    const [reviewLoading, setReviewLoading] = useState(false);
    const [reviewError, setReviewError] = useState<string | null>(null);
    const [reviewSuccess, setReviewSuccess] = useState(false);

    const handleReviewSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setReviewLoading(true);
        setReviewError(null);
        setReviewSuccess(false);
        try {
            await createReview({
                toolId,
                rating: reviewRating,
                comment: reviewComment,
            });
            setReviewSuccess(true);
            setReviewComment('');
            setReviewRating(5);
            onReviewAdded();
        } catch (err: any) {
            setReviewError(err.message || 'Failed to add review');
        } finally {
            setReviewLoading(false);
        }
    };

    return (
        <form onSubmit={handleReviewSubmit} style={{ marginTop: '2rem', padding: 16, borderRadius: 8 }}>
            <h3 style={{ marginTop: '2rem' }}>Add Review</h3>
            <div style={{ marginBottom: 8 }}>
                <label>Rating:
                    <select value={reviewRating} onChange={e => setReviewRating(Number(e.target.value))}>
                        {[5, 4, 3, 2, 1].map(r => <option key={r} value={r}>{r}</option>)}
                    </select>
                </label>
            </div>
            <div style={{ marginBottom: 8 }}>
                <label>Comment:<br />
                    <textarea value={reviewComment} onChange={e => setReviewComment(e.target.value)} rows={2} style={{ width: '100%' }} />
                </label>
            </div>
            {reviewError && <div style={{ color: 'red', marginBottom: 8 }}>{reviewError}</div>}
            {reviewSuccess && <div style={{ color: 'green', marginBottom: 8 }}>Review added!</div>}
            <button type="submit" disabled={reviewLoading}>{reviewLoading ? 'Submitting...' : 'Add Review'}</button>
        </form>
    );
}
