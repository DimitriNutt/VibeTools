import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getToolById, type ToolDetailsDto } from '../api/toolsApi';
import ToolInfo from '../components/ToolInfo';
import ToolActions from '../components/ToolActions';
import ReviewList from '../components/ReviewList';
import ReviewForm from '../components/ReviewForm';

export default function ToolDetailsPage() {
    const { id } = useParams<{ id: string }>();
    const [tool, setTool] = useState<ToolDetailsDto | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const fetchTool = () => {
        if (!id) return;
        setLoading(true);
        getToolById(id)
            .then(setTool)
            .catch((err) => setError(err.message))
            .finally(() => setLoading(false));
    };

    useEffect(() => {
        fetchTool();
    }, [id]);

    if (loading) return <div>Loading tool details...</div>;
    if (error) return <div style={{ color: 'red' }}>{error}</div>;
    if (!tool) return <div>Tool not found.</div>;

    return (
        <div style={{ maxWidth: 1200, margin: '2rem auto' }}>

            <div style={{ display: 'flex', gap: '2rem', alignItems: 'flex-start' }}>
                <div style={{ flex: 1, minWidth: 0 }}>
                    <ToolInfo tool={tool} />
                    <div style={{ display: 'flex', justifyContent: 'center', marginBottom: '1.5rem' }}>
                        <ToolActions toolId={tool.id} />
                    </div>
                    <ReviewForm toolId={tool.id} onReviewAdded={fetchTool} />
                </div>
                <div style={{ flex: 1, minWidth: 0 }}>
                    <h3 style={{ marginTop: '2rem' }}>Reviews</h3>
                    <ReviewList reviews={tool.reviews} />
                </div>
            </div>
        </div>
    );
}