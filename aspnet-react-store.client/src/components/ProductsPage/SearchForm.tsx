import { Search as SearchIcon } from '@mui/icons-material';
import {
  FormControl,
  IconButton,
  Input,
  InputAdornment,
  InputLabel,
} from '@mui/material';

interface SearchFormProps {
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onClick: () => void;
}
export default function SearchForm({
  value,
  onChange,
  onClick,
}: SearchFormProps) {
  return (
    <div style={{ width: '70%', margin: '2rem' }}>
      <FormControl fullWidth>
        <InputLabel htmlFor='search-input'>Название товара</InputLabel>
        <Input
          id='search-input'
          value={value}
          onChange={onChange}
          endAdornment={
            <InputAdornment position='end'>
              <IconButton onClick={onClick}>
                <SearchIcon color='inherit' />
              </IconButton>
            </InputAdornment>
          }
        />
      </FormControl>
    </div>
  );
}
