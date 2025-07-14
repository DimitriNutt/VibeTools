
import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getToolById, updateTool, type ToolDetailsDto } from '../api/toolsApi';
import ToolForm from '../components/ToolForm';
import type { ToolFormValues } from '../components/ToolForm';

export default function ToolEditPage() {
    const { id } = useParams<{ id: string }>();
    const [tool, setTool] = useState<ToolDetailsDto | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [saving, setSaving] = useState(false);
    const [success, setSuccess] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        if (!id) return;
        setLoading(true);
        getToolById(id)
            .then((tool) => {
                setTool(tool);
            })
            .catch((err) => setError(err.message))
            .finally(() => setLoading(false));
    }, [id]);

    const handleSubmit = async (values: ToolFormValues) => {
        if (!id) return;
        setSaving(true);
        setError(null);
        setSuccess(false);
        try {
            await updateTool({
                id,
                name: values.name,
                category: values.category.split(',').map((c) => c.trim()).filter(Boolean),
                description: values.description,
            });
            setSuccess(true);
            setTimeout(() => navigate(`/tools/${id}`), 1000);
        } catch (err: any) {
            setError(err.message || 'Failed to update tool');
        } finally {
            setSaving(false);
        }
    };

    if (loading) return <div>Loading tool...</div>;
    if (error) return <div style={{ color: 'red' }}>{error}</div>;
    if (!tool) return <div>Tool not found.</div>;

    return (
        <div style={{ maxWidth: 400, margin: '2rem auto' }}>
            <h2>Edit Tool</h2>
            <ToolForm
                initialValues={{
                    name: tool.name || '',
                    category: (tool.category || []).join(', '),
                    description: tool.description || '',
                }}
                onSubmit={handleSubmit}
                loading={saving}
                error={error}
                submitLabel="Save Changes"
            />
            {success && <div style={{ color: 'green', marginTop: 12 }}>Tool updated!</div>}
        </div>
    );
}