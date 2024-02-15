import { TextField, Typography } from '@mui/material';
import React, { useEffect, useState } from 'react';

interface ProfileFieldProps {
  title: string;
  value?: string | null | undefined;
  editing?: boolean;
  id: string;
  isRequired?: boolean;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function ProfileField({
  title,
  value,
  editing,
  id,
  isRequired,
  onChange,
}: ProfileFieldProps) {
  const requiredMessage = 'Поле обязательно для заполнения';

  const [error, setError] = useState<string>();

  const handleBlur = (_: React.FocusEvent<HTMLInputElement>) => {
    if (isRequired && !value?.trim()) setError(requiredMessage);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange!(e);
    setError('');
  };

  useEffect(() => {
    if (isRequired && value?.trim()) setError('');
  }, [value]);

  return (
    <div
      style={{
        display: 'flex',
        flexDirection: 'row',
        marginTop: '1rem',
      }}
    >
      <div style={{ display: 'flex', flexDirection: 'column' }}>
        <Typography variant='h6' fontWeight='400'>
          {title}
        </Typography>
      </div>
      <div
        style={{
          marginLeft: 'auto',
        }}
      >
        {!editing && (
          <Typography variant='body1' sx={{ color: 'grey', mb: '24px' }}>
            {value ?? 'Не указано'}
          </Typography>
        )}
        {editing && (
          <TextField
            name={id}
            id={id}
            variant='filled'
            value={value ?? ''}
            onChange={handleChange}
            onBlur={handleBlur}
            size='small'
            hiddenLabel
            helperText={error}
            error={!!error}
            required={isRequired}
            placeholder={title ?? ''}
            sx={{ width: '280px', height: '48px' }}
          />
        )}
      </div>
    </div>
  );
}
