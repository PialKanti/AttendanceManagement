export function containsUpperCase(str) {
    const regex = /[A-Z]/;
    return regex.test(str);
}

export function containsLowerCase(str) {
    const regex = /[a-z]/;
    return regex.test(str);
}

export function containsDigit(str) {
    const regex = /\d/;
    return regex.test(str);
}

export function containsNonAlphanumeric(str) {
    const regex = /[^a-zA-Z0-9]/;
    return regex.test(str);
}

export function containsUniqueCharacters(str) {
    const charSet = new Set(str);
    return charSet.size === str.length;
}