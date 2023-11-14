import { Navigate, Route, Routes } from 'react-router-dom'
import LandingPage from './pages/landing-page'
import SignUp from './pages/SignUp'
import SignIn from './pages/SignIn'
import Settings from './pages/Settings/Settings'
import ProfileSettings from './pages/Settings/profileSettings'
import AppearenceSettings from './pages/Settings/appearenceSettings'
import Home from './pages/home'
import Navigation from './pages/navigation'
import ErrorPage from './pages/error-page'

const App = () => {
  return (
    <div className="text-foreground bg-background">
      <Routes>
        <Route path="/">
          <Route index element={<LandingPage />} />
          <Route path="sign-in" element={<SignIn />} />
          <Route path="sign-up" element={<SignUp />} />
          <Route path="*" element={<ErrorPage />} />
          <Route path="app" element={<Navigation />}>
            <Route path="home" element={<Home />} />
            <Route path="characters" element={<h1>Characters</h1>} />
            <Route path="systems" element={<h1>Systems</h1>} />
          </Route>
          <Route path="settings" element={<Settings />}>
            <Route index element={<Navigate to="profile" replace />} />
            <Route index path="profile" element={<ProfileSettings />}></Route>
            <Route path="appearence" element={<AppearenceSettings />}></Route>
          </Route>
        </Route>
      </Routes>
    </div>
  )
}

export default App
