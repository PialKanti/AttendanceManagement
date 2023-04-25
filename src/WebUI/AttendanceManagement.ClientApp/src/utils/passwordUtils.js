import { containsUpperCase, containsLowerCase, containsNonAlphanumeric, containsDigit, containsUniqueCharacters } from '@/utils/StringUtils';

export function validatePassword(password) {
    if (containsUpperCase(password) === false) {
        return false;
    }
    if (containsLowerCase(password) === false) {
        return false;
    }
    if (containsDigit(password) === false) {
        return false;
    }
    if (containsNonAlphanumeric(password) === false) {
        return false;
    }
    if (containsUniqueCharacters(password) === false) {
        return false;
    }

    return true;
}