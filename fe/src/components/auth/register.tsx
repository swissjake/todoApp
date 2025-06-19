import { Button } from "../button";
import { Input } from "../input";

const Register = () => {
  return (
    <div className="flex h-screen w-screen items-center justify-center">
      <div className="flex flex-col items-center justify-center w-full max-w-[400px]">
        <h1 className="text-2xl font-bold">Register</h1>
        <form className="flex w-full flex-col items-center justify-center space-y-4 mt-4">
          <Input required type="text" placeholder="Name" />
          <Input required type="text" placeholder="Email" />
          <Input required type="password" placeholder="Password" />
          <Button className="w-full">Register</Button>
        </form>
      </div>
    </div>
  );
};

export default Register;
