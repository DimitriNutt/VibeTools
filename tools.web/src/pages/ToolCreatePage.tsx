
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { createTool } from '../api/toolsApi';
import ToolForm from '../components/ToolForm';
import type { ToolFormValues } from '../components/ToolForm';

export default function ToolCreatePage() {
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (values: ToolFormValues) => {
        setLoading(true);
        setError(null);
        try {
            await createTool({
                name: values.name,
                category: values.category.split(',').map((c) => c.trim()).filter(Boolean),
                description: values.description,
            });
            navigate('/tools');
        } catch (err: any) {
            setError(err.message || 'Failed to create tool');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div style={{ maxWidth: 400, margin: '2rem auto' }}>
            <h2>Add New Tool</h2>
            <ToolForm
                onSubmit={handleSubmit}
                loading={loading}
                error={error}
                submitLabel="Add Tool"
            />
        </div>
    );
}