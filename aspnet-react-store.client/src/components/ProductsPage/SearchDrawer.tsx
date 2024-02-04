import { Drawer } from '@mui/material';
import { useEffect, useState } from 'react';
import SearchForm from './SearchForm';

interface SearchDrawerProps {
  show: boolean;
  handleClose: () => void;
  searchText?: string;
}

export default function SearchDrawer({
  show,
  handleClose,
  searchText,
}: SearchDrawerProps) {
  const [searchInputValue, setSearchInputValue] = useState('');

  const handleSearch = () => {
    if (searchInputValue && searchInputValue.trim() !== '') {
      window.location.href =
        '/products?searchText=' + encodeURIComponent(searchInputValue.trim());
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchInputValue(e.target.value);
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      handleSearch();
    }
  };

  useEffect(() => {
    if (searchText) setSearchInputValue(searchText);
  }, []);

  return (
    <Drawer
      anchor='top'
      open={show}
      onClose={handleClose}
      onKeyDown={handleKeyDown}
    >
      <SearchForm
        value={searchInputValue}
        onChange={handleInputChange}
        onClick={handleSearch}
      />
    </Drawer>
  );
}
