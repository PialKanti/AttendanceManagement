<template>
    <div class="row">
        <div class="col"></div>
        <div class="col">
            <form>
                <div class="mb-3">
                    <label for="username" class="form-label">Username</label>
                    <input type="text" class="form-control" id="username" aria-describedby="usernameHelp">
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <input type="password" class="form-control" id="password">
                </div>
                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="rememberCheckbox">
                    <label class="form-check-label" for="rememberCheckbox">Remember me</label>
                </div>
                <button type="submit" class="btn btn-primary">Login</button>
            </form>
        </div>
        <div class="col"></div>
    </div>
</template>

<script>
export default {
    name: 'LoginForm'
}
</script>

<style scoped></style>