import { useState } from 'react';

export interface ToolFormValues {
    name: string;
    category: string;
    description: string;
}

interface ToolFormProps {
    initialValues?: ToolFormValues;
    onSubmit: (values: ToolFormValues) => Promise<void>;
    loading?: boolean;
    error?: string | null;
    submitLabel?: string;
}

export default function ToolForm({
    initialValues = { name: '', category: '', description: '' },
    onSubmit,
    loading = false,
    error = null,
    submitLabel = 'Submit',
}: ToolFormProps) {
    const [values, setValues] = useState<ToolFormValues>(initialValues);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setValues((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await onSubmit(values);
    };

    return (
        <form onSubmit={handleSubmit}>
            <div style={{ marginBottom: 12 }}>
                <input type="text" name="name" placeholder="Name" value={values.name} onChange={handleChange} required style={{ width: '100%', padding: 8, fontSize: 16 }} />
            </div>
            <div style={{ marginBottom: 12 }}>
                <input type="text" name="category" placeholder="Category (comma separated)" value={values.category} onChange={handleChange} style={{ width: '100%', padding: 8, fontSize: 16 }} />
            </div>
            <div style={{ marginBottom: 12 }}>
                <textarea name="description" placeholder="Description" value={values.description} onChange={handleChange} rows={6} style={{ width: '100%', padding: 8, fontSize: 16 }} />
            </div>
            {error && <div style={{ color: 'red', marginBottom: 12 }}>{error}</div>}
            <button type="submit" disabled={loading}>{loading ? 'Saving...' : submitLabel}</button>
        </form>
    );
}
